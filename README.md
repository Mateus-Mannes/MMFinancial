# MMFinancial

development from Alura Challenge:
https://www.alura.com.br/challenges  (Desafio Back-End - Edição 3)

Objective:
develop a web app to analyze financial transactions

Tecnologies:
MVC .NET, using ABP framework (https://abp.io/)

# Runing the aplication:

Pre-Requirements:
The following tools should be installed on your development machine:

An IDE (e.g. Visual Studio) that supports .NET 6.0+ development.

Node v12 or v14

Yarn v1.20+ (not v2) 1 or npm v6+ (already installed with Node)

Abp documentation for running the application:

https://docs.abp.io/en/abp/4.4/Getting-Started-Running-Solution?UI=MVC&DB=EF&Tiered=No

# Functionalities:

-> Structure: The app follows Abp structure based on DDD design. Most of it I got reference from the Abp tutorial for web apps (https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF) and their free ebook about DDD (https://abp.io/books/implementing-domain-driven-design?ref=doc&_ga=2.195406762.744762924.1650882917-1122402396.1640382662)

 -> User management: the user registragion requires an email and a user name. The password for login is send for the registered email. Here I used abp Identity (https://docs.abp.io/en/abp/latest/Modules/IdentityServer) and account (https://docs.abp.io/en/abp/latest/Modules/Account) modules. I customized then by overriding it. The Services and Razor models that I overrided from abp has the prefix "App", for example: AppAccountAppService.cs.
 
 -> Authorization: the application has the admin role with full control and a default user role. All using abp fetures for authorization (https://docs.abp.io/en/abp/latest/Authorization)
 
 -> Transactions Features: The app has two main features involving the financial transactions analysis (I'm not going to explicit all the business rules here. This details are in the alura challange page https://www.alura.com.br/challenges/back-end-3/semana-01-upload-tratamento-arquivos-csv):
  
   --- Transactions/Upload route: here is possible to upload Files (Csv or Xml) with trasanctions that will be saved on the database. in the project there are two examples of these files (file1.csv, file2.xml). All the uploads are listed in this page too, where you can se de details of the upload, delete it or download the file. The storage of the files is implemented by blob storage in the sql server. Here is a article that a used to learn that: https://volosoft.com/blog/File-Upload-Download-with-BLOB-Storage-in-ASP.NET-Core-and-ABP (abp docs: https://docs.abp.io/en/abp/4.4/Blob-Storing)
   
   --- Transaction/Suspect route: here the user can choose a month to analyze transactions considered suspect based on the rules from the challange.
   
# Deployment

Deployed at https://mmfinancialapi.azurewebsites.net/
tutorial reference: https://community.abp.io/posts/deploying-abp-applications-to-azure-ztvp6p57

# Development process:

# week 1

Creation of a form to upload files

Read uploaded file and save to Data Base (Blob Storage)

Registration of trasactions to Data Base

Add a an uploads history table in the page

# week 2

Developing users crud

Authorization

Registration

Transactions Details

# week 3

Made Suspect Transactions analysis

Creating Tests

Supporting Xml Files

Deploying

