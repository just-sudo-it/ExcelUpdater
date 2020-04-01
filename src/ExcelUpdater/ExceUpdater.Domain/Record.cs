using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace ExceUpdater.Domain
{
    public class Record
    {
        public static Record Create(List<string> values)
        {
            var index = 0;
            Record rec = new Record();
            var properties = rec.GetType().GetProperties();

            foreach (var property in properties)
            {
                property.GetSetMethod().Invoke(rec, new object[] { rec.TryParseVal(values[index++])} );
            }
            return rec;
        }

        public Record() {     
        }
   
        [Required]
        public DateTime My_Date { get; set; }
        [Required]
        public string My_Id { get; set; }
        public float Time1 { get; set; }
        public float Time2 { get; set; }
        public float Time3 { get; set; }
        public float Time4 { get; set; }
        public float Time5 { get; set; }
        public float Time6 { get; set; }
        public float Time7 { get; set; }
        public float Time8 { get; set; }
        public float Time9 { get; set; }
        public float Time10 { get; set; }
        public float Time11 { get; set; }
        public float Time12 { get; set; }
        public float Time13 { get; set; }
        public float Time14 { get; set; }
        public float Time15 { get; set; }
        public float Time16 { get; set; }
        public float Time17 { get; set; }
        public float Time18 { get; set; }
        public float Time19 { get; set; }
        public float Time20 { get; set; }
        public float Time21 { get; set; }
        public float Time22 { get; set; }
        public float Time23 { get; set; }
        public float Time24 { get; set; }

        //UTILITY METHODS
        public object TryParseVal(string value)
        {
            if (DateTime.TryParseExact(value, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate;
            }
            else if (value.Length > 10 && IsDigitsOnly(value))
            {
                return value;
            }
            else if (float.TryParse(value, out var parsedTime))
            {
                return parsedTime;
            }
            return null;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
