
# Email-to-Dynamo
Automation: AWS serverless solution to catch, extract, and store the data sent via emails in Amazon Dynamo NoSQL database.


## Configure

 - Open Gmail -> See all Settings
 - Forwarding and POP/IMAP -> Enable IMAP
 - Navigate to Google Sheets 
 - Extensions -> Apps Script
 - Write the code provided below ->
 


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
- set a trigger to execute script every 1-5 minutes. 
    (this fetches the data from the emails and sends it to the API gateway).
- Put your deployed api-gateway url in place of "your_api_gateway_url" and you're good to go.
