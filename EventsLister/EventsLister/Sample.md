# EventsLister
This project shows the events of FHTW in command line.

## 1) Initial setup

- Reads the page [events](https://www.technikum-wien.at/newsroom/veranstaltungen/) into a 
  string in your application using HttpClient.
- Install the nuget HtmlAgilityPack
- Create an HtmlDocument and load the content of the page from the string into the document-object.
- use select nodes to find the text of the headings
- write the headings (first page only, paging can be ignored) to the console.

## 2) Write the dates
- add to the console the information when the event happens.

## 3) Add the count of items
- for better UX create a headline with the count of events in the text

## 4) Allow users to filter

- add argument handling to be able to filter for events

## 5) interactive mode

- create an interactive mode so that filtering can be added
- the data should be loaded only once and cached
- interactive mode is the default behaviour, but can be deactivated by argument /nointeractive
- commands: exit, list, filter

## 6) unit tests

- write unit test -> probably don't possible if you don't have it in mind the first place