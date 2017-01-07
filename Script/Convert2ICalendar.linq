<Query Kind="Statements" />

var data = @"
8/8　hoge

8/9　fuga

12/10 piyo


";
var reg = new Regex(@"^(?<date>\d{1,2}/\d{1,2})(?<content>.+)$", RegexOptions.Multiline);
var timeStr = DateTime.Now.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
var contents = reg.Matches(data)
	.Cast<Match>()
	.ToDictionary(x => DateTime.Parse($"2016/{x.Groups["date"].Value}"), x => x.Groups["content"].Value.Trim())
	.OrderBy(x => x.Key)
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