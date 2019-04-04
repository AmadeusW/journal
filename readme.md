Journal
=

Logging tool for everything. Quickly feed the data by typing. Retrieve and analyze recorded data.

**Essential**
- Record stores properties
- Commandline either invoke commands one by one
- or commandline interface for recording and querying
- Create calendar events
- Set custom date to recorded data

**Nice to have**
- Record autocompletes properties
- Record does not create new properties
- Record behavior is configurable
- Make it possible to fix the list of things I'm recording, so that typos don't happen (or use –f)
- Or alternatively, if a new subject is created, we check if there is another subject which might be a good fit.
- Authorization, encryption

**Extensibility**
- Kiril's extremely nice and easy extensibility: https://github.com/KirillOsenkov/QuickInfo/blob/master/src/QuickInfo/Engine.cs

**Examples**
The syntax of notes is `verb parameters @date #place`

```
run (implicitly today)
run yesterday

plan climb @friday
plan email foo bar baz (implicitly today)
plan email foo bar baz @tomorrow
plan email foo bar baz @3.15

show [topic] [@when]
find [topic or value] [@when]

show today
show week
show run // ? how do I distinguish this from today/week/all/last/etc?
show all run // all runs
show all run -t -1m // all runs in last month
show run -5
show run -5m
show run 3.20..3.25
show run (3/20,3/25]
show run (,-3m]
show run 3/20..
show run ..3/20
show last run // only last run

verbose format where date is at the end
plan email foo bar baz -t 3.15
log climb -t -2d

Note record https://github.com/KirillOsenkov/QuickInfo/blob/master/src/QuickInfo/Engine.cs // Should add the hyperlink to my "record" set of notes
```
