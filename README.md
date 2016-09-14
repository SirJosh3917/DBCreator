# DBCreator
Data Base Creator. You can use this to create a database, with lots of objects stored, and load them back correctly.

## A breif guide and tutorial on how to use DBCreator
First, you'll want to download the DLL from DBCreator/bin/debug, or one of the downloads below:

//TODO: Nuget and Mediafire

Then, you'll want to create a database with a name.

`Database Test = new Database("Test");`

Now, we'll learn how to create tables.

A table is a chunk within the database that stores tons of DatabaseObjects.

Here's a ASCII flowchart:

```
         DatabaseObject
DATABASE |
|   |  | |
|   |  Table--DatabaseObject
|   Table-DatabaseObject
Table |
|   | DatabaseObject
|   DatabaseObject
DatabaseObject
```

Every database can have an infinite amount of tables (depending on your computer's amount of space).
And every table can have an infinite amount of DatabaseObjects (depending on your computer's amount of space).

So we can create tables by doing

`Test.CreateTable("CustomTable");`

And we're done. Now we can store DatabaseObjects inside of that table, by doing:

`Test.Set("CustomTable", "CustomDatabaseObject", int.MaxValue);`

There, now we've created a table, with a DatabaseObject named CustomDatabaseObject, that's storing the maximum value of an integer. Well done!

Now you can go along and make millions of tables and add DatabaseObjects to it!

But once you're done making your table, you'll want to save it, with a quick

`Test.Save();`

And that's all there is to it!

Next time, when you do `Database Test = new Datbase("Test");` it will automatically load the database for you, so you can start accessing these values right away!