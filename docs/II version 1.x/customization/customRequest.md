# CustomRequest

Here is a list of methods and examples of working with Custom Requests

## Overview
WP REST Api can be modified and extended by plugins (Woocommerce, Contact Form 7, ACF and others), so Custom requests allow you to create non-standard requests.
Before send requests you must create DTO Model of your request.
Here is an example with Contact Form 7 plugin

## DTO Model
```C#
public class ContactFormItem
{
     public int? id;
     public string title;
     public string slug;
     public string locale;
}
```

## Get
```C#
var forms = client.CustomRequest.Get<IEnumerable<ContactFormItem>>("contact-form-7/v1/contact-forms");
```

## Create
```C#
//requires two T parameters: first - input model, second - output model
var forms = client.CustomRequest.Create<ContactFormItem,ContactFormItem>("contact-form-7/v1/contact-forms",new ContactFormItem() { title = "test" });
```

## Update
```C#
//requires two T parameters: first - input model, second - output model
var forms = client.CustomRequest.Update<ContactFormItem,ContactFormItem>("contact-form-7/v1/contact-forms/123",new ContactFormItem() { title = "test" });
```

## Delete
```C#
var forms = client.CustomRequest.Delete("contact-form-7/v1/contact-forms/123");
```