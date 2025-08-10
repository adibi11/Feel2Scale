This project will use OpenAI API to get data from the user and and suggest scales, instruments, effects etc.
The project is running on C# .NET and is in progress.
In order to run the project locally on your computer you have to have an access key from OpenAI API and a postgres server running on docker

I created a dedicated project for the configuration, so that debugging or deployment will be easier to do. 
You have to create an appsettings.json file and create there a connection string and add the API key.

In the future I will add a yml file to run the API and the server on docker without having to open the API in an IDE and it will make work much easier.
