<Query Kind="Statements" />

var data = @"
1/1 xxx
8/8　hoge

8/9　fuga

12/10 piyo
1/1 piyo2
1/10 piyo3


";
var reg = new Regex(@"^(?<date>\d{1,2}/\d{1,2})(?<content>.+)$", RegexOptions.Multiline);
var timeStr = DateTime.Now.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
var currentDate = new DateTime(2016, 1, 1);
var contents = reg.Matches(data)
	.Cast<Match>()
	.ToDictionary(x =>
	{
		var date = DateTime.Parse($"{currentDate.Year}/{x.Groups["date"].Value}");
		currentDate = currentDate > date ? date.AddYears(1) : date;
		return currentDate;
	}, x => x.Groups["content"].Value.Trim())
	.Select(x =>
$@"BEGIN:VEVENT
DTSTART;VALUE=DATE:{x.Key.ToString("yyyyMMdd")}
DTEND;VALUE=DATE:{x.Key.AddDays(1).ToString("yyyyMMdd")}
DTSTAMP:{timeStr}
UID:
CREATED:{timeStr}
DESCRIPTION:
LAST-MODIFIED:20170107T085120Z
LOCATION:
SEQUENCE:0
STATUS:CONFIRMED
SUMMARY:{x.Value}
TRANSP:TRANSPARENT
END:VEVENT"
	);

$@"BEGIN:VCALENDAR
PRODID:-//Tatsuya//Tatsuya Calendar 1.0.0//JP
VERSION:2.0
CALSCALE:GREGORIAN
METHOD:PUBLISH
X-WR-CALNAME:Diary
X-WR-TIMEZONE:Asia/Tokyo
X-WR-CALDESC:
{string.Join(Environment.NewLine, contents)}
END:VCALENDAR
"
	.Dump();