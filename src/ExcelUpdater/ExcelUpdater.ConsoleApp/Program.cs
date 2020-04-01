using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ExcelUpdater.Data;
using NPOI.SS.UserModel;
using ExceUpdater.Domain;
using NPOI.XSSF.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;
using Serilog;
using Serilog.Events;
using System.Globalization;

namespace ExcelUpdater.ConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler{UseCookies = true});
        
        static async Task<int> Main()
        {
            IConfiguration Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false,  true)
                .AddJsonFile("appsettings.local.json",true,true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Config)
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

            DbContextOptionsBuilder<RecordContext> builder = new DbContextOptionsBuilder<RecordContext>()
                .UseSqlServer(Config.GetConnectionString("ExcelUpdater"));
            
            using RecordContext ctx = new RecordContext(builder.Options);

            #region SET DEFAULT HTTP-REQUEST HEADERS
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            #endregion

            #region SET LOGIN-FORM VALUES
            KeyValuePair<string, string>[] credentials =
            {
                new KeyValuePair<string,string>("EmailAddress",Config["ClientCredential:Email"]),
                new KeyValuePair<string, string>("Password",Config["ClientCredential:Password"])
            };
            #endregion

            try
            {   
                //AUTHENTICATE USER       
                await PostToPageAsync(new Uri(Config["LoginUrl"]), credentials);
                //DOWNLOAD EXCEL TO LOCAL PATH
                await DownloadFileAsync(new Uri(Config["ExcelUrl"]), Config["ExcelPath"]);
              
                var excelStream = await GetStreamAsync(new Uri(Config["ExcelUrl"]));
              
                Record latestRecord = await ctx.Records.OrderBy(x => x.My_Date).LastOrDefaultAsync();

                ISheet sheet = new XSSFWorkbook(excelStream).GetSheetAt(0);
                List<Record> recsToInsert = new List<Record>();

                Log.Information("[Excel] Parsing .xlsx file..");
                for (int i = 3; i < sheet.LastRowNum; i++)
                {
                    var rowValues = new List<string>();
                    IRow row = sheet.GetRow(i);

                    if (DateTime.TryParseExact(row.GetCell(0).ToString().Trim(), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var Date) && latestRecord !=null)
                    {
                        if(Date< latestRecord.My_Date)
                            continue;
                    }
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        rowValues.Add(row.GetCell(j).ToString().Trim());
                    }
                    
                    Record current = Record.Create(rowValues);
                    recsToInsert.Add(current);
                }
                //UPDATE DATABASE
                Log.Information(string.Format("[DB]Inserting {0} records in batches",recsToInsert.Count));

                ctx.BulkInsertOrUpdate(recsToInsert);

                Log.Information("[DB] Records Inserted");
            }
            catch (Exception e)
            {
                Log.Fatal(e, "[ERROR] Application failed");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            return 0;
        }

        private static async Task<Stream> GetStreamAsync(Uri uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            Log.Information(string.Format("[Stream file] Status code: {0}  \t Phrase : {1}",response.StatusCode,response.ReasonPhrase));

            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                Log.Information("Getting excel stream...");
                return responseStream;
            }
            Log.Debug("Failed to get excel stream");
            return null;
        }

        private static async Task PostToPageAsync(Uri uri, KeyValuePair<string, string>[] formValues)
        {
            using HttpResponseMessage response = await client.PostAsync(uri, new FormUrlEncodedContent(formValues) );

            if (response.IsSuccessStatusCode) {
                Log.Information(string.Format("[Post] Status code: {0}  \t Phrase : {1}", response.StatusCode, response.ReasonPhrase));
                return;
            }
            else
            {
                Log.Debug(string.Format("[Post] Failed to authenticate.Status code: {0}  \t Phrase : {1}", response.StatusCode, response.ReasonPhrase))
            }
        }

        private static async Task  DownloadFileAsync(Uri uri, string toPath)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            Log.Information(string.Format("[Download file] Status code: {0}  \t Phrase : {1}", response.StatusCode, response.ReasonPhrase)); 

            if (response.IsSuccessStatusCode)
            {
                using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using Stream outputStream = File.Open(toPath, FileMode.Create, FileAccess.Write);
                    await responseStream.CopyToAsync(outputStream);
                    Log.Information(string.Format("[Download file] : File Downloaded at :{0}",toPath));
                    return;
                }
            }
            Log.Debug("[Download file]: Failed to download file");
        }

        private static void XlsxToCsv(string xlsxPath, string csvPath)
        {
            using var inStream = new BufferedStream(new FileStream(xlsxPath, FileMode.Open, FileAccess.Read, FileShare.Read));
            
            XSSFWorkbook book = new XSSFWorkbook(inStream);
            ISheet sheet = book.GetSheetAt(0);

            using (var streamWriter = new StreamWriter((new FileStream(csvPath, FileMode.CreateNew))))
            {
                StringBuilder buffer = new StringBuilder();
                //(i=3) SKIP  SHEET HEADERS
                for (int i = 3; i < sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        buffer.Append(row.GetCell(j).ToString().Trim());
                        buffer.Append(",");
                    }
                    buffer.Append("\n");

                    streamWriter.Write(buffer.ToString());
                    buffer.Clear();
                }
            }
        }
    }
}
