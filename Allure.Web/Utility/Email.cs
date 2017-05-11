using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Allure.Web.Utility
{
    public class Email
    {
        public static void SendEmail(string to, string subject, string body)
        {

            var conn_config = System.Configuration.ConfigurationManager.ConnectionStrings["Allure"];
            if (conn_config != null)
            {
                string connstring = conn_config.ConnectionString;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connstring))
                    {
                        conn.Open();
                        SqlCommand sqlcmd = conn.CreateCommand();

                        string sql = @"exec msdb.dbo.sp_send_dbmail
                            @profile_name='Allure_Mail',   --配置文件
                            @recipients='{0}',	--收件者
                            @subject='{1}',  --主旨
                            @body='{2}', --内文
                            --@query='select getdate()',  --还可以下查询式哦
                            --@file_attachments='C:\test.txt',  --夹档
                            --@attach_query_result_as_file=1,  --把查询的结果设为附件夹文件，不设的话就是在mail内容中看到啰
                            --@body_format=TEXT    --使用text格式
                            @body_format=HTML	--也可以使用HTML格式";

                        sql = string.Format(sql, to, subject, body);
                        sqlcmd.CommandText = sql;
                        
                        sqlcmd.ExecuteNonQuery();

                    }
                }
                catch (Exception)
                {
                }
                
            }

            //SmtpClient SmtpServer = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
            //var mail = new MailMessage();
            //mail.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["UserName"]);
            //mail.To.Add(to);
            //mail.Subject = subject;
            //mail.IsBodyHtml = true;
            //string htmlBody;
            //htmlBody = body;
            //mail.Body = htmlBody;
            //SmtpServer.Port = 587;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["UserName"], System.Configuration.ConfigurationManager.AppSettings["Password"]);
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);
            //SmtpServer.Dispose();
        }
    }
}