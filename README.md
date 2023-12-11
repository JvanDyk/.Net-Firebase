# .Net-Firebase Admin SDK
.Net using Firebase

## To get started

### Step 1:
Replace Booking.Shared/Authentication/ServiceAccount.json,
that you can download by clicking on Generate new private key at
https://console.firebase.google.com/u/0/project/{your_project_name}/settings/serviceaccounts/adminsdk
Note: rename the file back to ServiceAccount.json

### Step 2:
In the startup file at the top it is setting the path to this file in the ENVIRONMENT variables as GOOGLE_APPLICATION_CREDENTIALS.
Firebase libary uses this ENVIRONMENT variable to connect to it's servers.

:fire::fire::fire:!Done!:fire::fire::fire:

What's cool about this projec is that Firebase will automatically create entites based of your code.
So whatever you want with it. Rename the project, add, update or delete entites, make microservices!
