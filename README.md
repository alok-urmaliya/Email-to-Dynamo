
# Email-to-Dynamo

An AWS Serverless Api to fetch and store email data in dynamo database.


## Configure

 - Open Gmail -> See all Settings
 - Forwarding and POP/IMAP -> Enable IMAP
 - open google Sheets 
 - Extensions -> Apps Script
 - write the below code->
 


```bash
  function sendEmailDataToApi() {
  var threads = GmailApp.search('label:webhook');
  for (var i = 0; i < threads.length; i++) 
  {
    var messages = threads[i].getMessages();
    for (var j = 0; j < messages.length; j++) 
    {
      var emailData = 
      {
        'subject': messages[j].getSubject(),
        'body': messages[j].getBody(),
        'from': messages[j].getFrom(),
        'to': messages[j].getTo(),
        'date': messages[j].getDate()
      };
      var options = 
      {
        'method' : 'post',
        'contentType': 'application/json',
        'payload' : JSON.stringify(emailData)
      };
      UrlFetchApp.fetch('your_api_gateway_url', options);
      threads[i].removeLabel(GmailApp.getUserLabelByName('webhook'));
    }
  }
  return emailData;
}
```

- set a trigger to execute script every 5 minutes. 
