namespace QuizApp.Domain.Shared
{
    public static class EmailMessageExample
    {
        private static string subject = "New User Registration";

        public static string GetEmailBody(string firstname, string otpCode)
        {
            return @"<!DOCTYPE html>
        <html>
            <head>
                <meta charset='utf-8'>
                <title>OTP Verification Code</title>
                <style type='text/css'>
                    body {
                        font-family: Arial, Helvetica, sans-serif;
                    }
                    h1, h2 {
                        color: #333333;
                        text-align: center;
                    }
                    h1 {
                        font-size: 32px;
                    }
                    h2 {
                        font-size: 48px;
                    }
                    p {
                        color: #666666;
                        font-size: 16px;
                        line-height: 1.5;
                        margin: 20px 0;
                        text-align: center;
                    }
                </style>
            </head>
            <body>
                <h1>OTP Verification Code</h1>
                <p>Dear " + firstname + @",</p>
                <p>Your OTP verification code is:</p>
                    <h2 style=""background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;"">" + otpCode + @"</h2>

                < p > Please enter this code to complete your verification process.</ p >
            </ body >
        </ html > ";
        }

        public static string GetEmailSubject()
        {
            return subject;
        }
    }
}
