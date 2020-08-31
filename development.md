# Development notes

Extensible composition
String commands are handled by a composed processor
The processor attempts to parse a string command
The processor may designated the command as read or write. all writes should be persisted. persistence of reads is optional
Writes persist data into the abstract store. One store might be implemented with https://www.litedb.org/
Read commands: show, list, etc. before, after