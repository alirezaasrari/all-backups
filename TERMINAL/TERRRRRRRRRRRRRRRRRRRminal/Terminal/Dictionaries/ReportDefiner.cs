namespace Terminal.Dictionaries
{
    public class ReportDefiner
    {
        ReportDefiner()
        {

            ReportManager reportCodeManager = new();
            // fashionable errors are defined here
            reportCodeManager.AddReportCode(1, "خطا در پروسه");
            reportCodeManager.AddReportCode(2, "ورودی اشتباه");
            reportCodeManager.AddReportCode(3, "فایل پیدا نشد");
            // here we define the command codes and descriptions
            reportCodeManager.AddReportCode(4, "مصرف فعلی");
            reportCodeManager.AddReportCode(5, "مصرف روزانه");
            reportCodeManager.AddReportCode(6, "پروفایل مصرف ساعتی");
            reportCodeManager.AddReportCode(7, "پروفایل مصرف ماهانه");
            reportCodeManager.AddReportCode(8, "مصرف کل");
            reportCodeManager.AddReportCode(9, "simcard balance");
            reportCodeManager.AddReportCode(10, "set setting");
            reportCodeManager.AddReportCode(11, "set setting error");
            reportCodeManager.AddReportCode(12, "get setting");

            reportCodeManager.AddReportCode(13, "fota");
            reportCodeManager.AddReportCode(14, "fota updating");
            reportCodeManager.AddReportCode(15, "fota ok");
            reportCodeManager.AddReportCode(16, "fota error");

            reportCodeManager.AddReportCode(17, "midnight data");
            reportCodeManager.AddReportCode(18, "hourly profile data");
            reportCodeManager.AddReportCode(19, "month data");
            reportCodeManager.AddReportCode(20, "queue data");
            reportCodeManager.AddReportCode(21, "hourly queue data");
            reportCodeManager.AddReportCode(22, "check request");
            reportCodeManager.AddReportCode(23, "alarm");
            reportCodeManager.AddReportCode(24, "test primary server");
            reportCodeManager.AddReportCode(25, "test backup server");
            reportCodeManager.AddReportCode(26, "data ok");
            reportCodeManager.AddReportCode(27, "crc error");
            // here we will define settings 
            reportCodeManager.AddReportCode(100, "Primary Server IP(URL)");
            reportCodeManager.AddReportCode(101, "sms sender");
            reportCodeManager.AddReportCode(103, "check server request interval");
            reportCodeManager.AddReportCode(104, "disable daylight saving");
            reportCodeManager.AddReportCode(105, "primary server port");
            reportCodeManager.AddReportCode(106, "time configure");
            reportCodeManager.AddReportCode(107, "reset alarms");
            reportCodeManager.AddReportCode(108, "send server timeout");
            reportCodeManager.AddReportCode(109, "backup server ip(url)");
            reportCodeManager.AddReportCode(110, "backup server port");
            reportCodeManager.AddReportCode(111, "enable-disable hourly profile");
            reportCodeManager.AddReportCode(115, "Http-tcp");
            reportCodeManager.AddReportCode(117, "close midnight offset");
            reportCodeManager.AddReportCode(118, "time zone");
            reportCodeManager.AddReportCode(119, "valve self clean");

            reportCodeManager.AddReportCode(120, "reference valume 1");
            reportCodeManager.AddReportCode(121, "reference valume 2");
            reportCodeManager.AddReportCode(122, "reference valume 3");
            reportCodeManager.AddReportCode(123, "referece valume 4");
            reportCodeManager.AddReportCode(124, "refernce valume 5");
            reportCodeManager.AddReportCode(125, "refernce valume 6");
            reportCodeManager.AddReportCode(126, "refernce valume 7");
            reportCodeManager.AddReportCode(127, "refernce valume 8");

            reportCodeManager.AddReportCode(140, "correction coefficient 1");
            reportCodeManager.AddReportCode(141, "correction coefficient 2");
            reportCodeManager.AddReportCode(142, "correction coefficient 3");
            reportCodeManager.AddReportCode(143, "correction coefficient 4");
            reportCodeManager.AddReportCode(144, "correction coefficient 5");
            reportCodeManager.AddReportCode(145, "correction coefficient 6");
            reportCodeManager.AddReportCode(146, "correction coefficient 7");
            reportCodeManager.AddReportCode(147, "correction coefficient 8");

            reportCodeManager.AddReportCode(160, "meter intitial usage 1");
            reportCodeManager.AddReportCode(161, "meter intitial usage 2");
            reportCodeManager.AddReportCode(162, "meter intitial usage 3");
            reportCodeManager.AddReportCode(163, "meter intitial usage 4");
            reportCodeManager.AddReportCode(164, "meter intitial usage 5");
            reportCodeManager.AddReportCode(165, "meter intitial usage 6");
            reportCodeManager.AddReportCode(166, "meter intitial usage 7");
            reportCodeManager.AddReportCode(167, "meter intitial usage 8");

            reportCodeManager.AddReportCode(180, "meter 1 serial number");
            reportCodeManager.AddReportCode(181, "meter 2 serial number");
            reportCodeManager.AddReportCode(182, "meter 3 serial number");
            reportCodeManager.AddReportCode(183, "meter 4 serial number");
            reportCodeManager.AddReportCode(184, "meter 5 serial number");
            reportCodeManager.AddReportCode(185, "meter 6 serial number");
            reportCodeManager.AddReportCode(186, "meter 7 serial number");
            reportCodeManager.AddReportCode(187, "meter 8 serial number");

            reportCodeManager.AddReportCode(200, "reset meter data 1");
            reportCodeManager.AddReportCode(201, "reset meter data 1");
            reportCodeManager.AddReportCode(202, "reset meter data 1");
            reportCodeManager.AddReportCode(203, "reset meter data 1");
            reportCodeManager.AddReportCode(204, "reset meter data 1");
            reportCodeManager.AddReportCode(205, "reset meter data 1");
            reportCodeManager.AddReportCode(206, "reset meter data 1");
            reportCodeManager.AddReportCode(207, "reset meter data 1");

            reportCodeManager.AddReportCode(250, "valve open period");
            reportCodeManager.AddReportCode(251, "valve close period");

            reportCodeManager.AddReportCode(220, "valve state 1");
            reportCodeManager.AddReportCode(221, "valve enable");
            reportCodeManager.AddReportCode(222, "valve driver type");

            reportCodeManager.AddReportCode(223, "valve state 2");
            reportCodeManager.AddReportCode(224, "valve state 3");
            reportCodeManager.AddReportCode(225, "valve state 4");
            reportCodeManager.AddReportCode(226, "valve state 5");
            reportCodeManager.AddReportCode(227, "valve state 6");
            reportCodeManager.AddReportCode(228, "valve state 7");
            reportCodeManager.AddReportCode(229, "valve state 8");

            reportCodeManager.AddReportCode(240, "remain usage balance 1");
            reportCodeManager.AddReportCode(241, "remain usage balance 2");
            reportCodeManager.AddReportCode(242, "remain usage balance 3");
            reportCodeManager.AddReportCode(243, "remain usage balance 4");
            reportCodeManager.AddReportCode(244, "remain usage balance 5");
            reportCodeManager.AddReportCode(245, "remain usage balance 6");
            reportCodeManager.AddReportCode(246, "remain usage balance 7");
            reportCodeManager.AddReportCode(247, "remain usage balance 8");

            reportCodeManager.AddReportCode(600, "burst flow threshold");
            reportCodeManager.AddReportCode(601, "burst time threshold");
            reportCodeManager.AddReportCode(602, "burst number of times threshold");
            reportCodeManager.AddReportCode(603, "leakage flow threshold");
            reportCodeManager.AddReportCode(604, "leakage timeout threshold");
            reportCodeManager.AddReportCode(605, "leakage number of times threshold");
            reportCodeManager.AddReportCode(606, "frozen timeout threshold");
            reportCodeManager.AddReportCode(607, "frozen degree threshold");
            reportCodeManager.AddReportCode(608, "reverse volume threshold");
            reportCodeManager.AddReportCode(609, "reverse timeout threshold");
            reportCodeManager.AddReportCode(610, "displacement acceleration threshold");
            reportCodeManager.AddReportCode(611, "displacement timeout threshold");

            // here is the end of the hex codes command
            string description = reportCodeManager.GetDescription(1);
            Console.WriteLine(description);
        }
    }
}
