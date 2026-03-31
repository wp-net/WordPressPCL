# HttpResponsePreProcessing

Sometimes it's necessary to pre-process the HttpResponseMessage before deserializeing it.

To do this, you can use the "HttpResponsePreProcessing" to pass a function to the WordPressClient.
This function takes the HttpResponse content string as a parameter, and has to return a string that will be deserialized.

```C#
// Add a HttpResponsePreProcessing function :
client.HttpResponsePreProcessing = (response) =>
{
    string updatedResponse = response;

    // Do something here on the updatedResponse

    return updatedResponse;
};
```

## WordPress on Azure / ISS

When deploying a WordPress website on Microsoft Azure, the REST API will add [unnecessary HTML before every POST request](https://social.msdn.microsoft.com/Forums/azure/en-US/8faac1fa-3d47-4149-aec0-f0a9a09f9744/php-wordpress-rest-api-prepending-html-with-location-header?forum=windowsazurewebsitespreview).

In order to correct this, you can add a HttpResponsePreProcessing function that will delete the HTML code that is preventing the deserialization of the JSON content:

```C#
client.HttpResponsePreProcessing = (responseString) =>
{
    var clearedString = responseString.Replace("\n", "");
    var regex = @"\<head(.+)body\>";
    return System.Text.RegularExpressions.Regex.Replace(clearedString, regex, "");
};
```
