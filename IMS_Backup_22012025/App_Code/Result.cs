﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Result
/// </summary>
public class Result
{
        public string ExceptionMessage { set; get; }
        public string ExceptionInnerMessage { set; get; }
        public string ExceptionStackTrace { set; get; }
        public string InnerExceptionStackTrace { set; get; }
        public bool Status { set; get; }
        public int RecordsAffected { set; get; }
        public string Message { set; get; }
        public int OperationID { set; get; }

        public static Result CreateFromException(string message, Exception ex)
        {
            Result opStatus = new Result
            {
                Status = false,
                Message = message
            };
            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = ex.InnerException == null ? "" : ex.InnerException.ToString();
                opStatus.InnerExceptionStackTrace = ex.InnerException == null ? "" : ex.InnerException.StackTrace;
            }
            return opStatus;
        }
}