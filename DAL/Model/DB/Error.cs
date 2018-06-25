using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public enum ErrorLevel { warning, critical, normal }

    public class Error
    {
        [Key]
        public Guid ErrorID { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public string Stack { get; set; }

        public string InnerExceptionMessage { get; set; }

        public string InnerExceptionStack { get; set; }

        public ErrorLevel Level { get; set; }

        public string UserID { get; set; }
    }
}