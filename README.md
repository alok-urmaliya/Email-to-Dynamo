# Email-to-DynamoDb
An AWS serverless API to extract data from gmail and store it in dynamoDb.
**Getting Started**
  To run this project you need to setup some things first.

**Gmail IMAP forwarding**
  -Open Gmail
  -Go to Settings
  -In the forwarding and POP/IMAP section Enable IMAP
**Run script to forward Email Data*
  -open Gmail
  -click on 9 dots icon on top right
  -open google sheets
  -Extensions=>AppsScript
  -write this code in the file
    ```gs
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
                UrlFetchApp.fetch('YOUR_AWS_API_GATEWAY_URL', options);
                threads[i].removeLabel(GmailApp.getUserLabelByName('webhook'));
              }
          }
        return emailData;
        }
    ```
   -Set a trigger to extecute function every 5 minutes.
   
  
