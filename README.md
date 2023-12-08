# Coffee Shop
This Project is format from two parts: .Net (Rest API) and Angular 15.9 ( web page UI)
What you can to on this web app is:
* Register (Sign Up)
* Login (Sign In)
* Subscribe to newsletter 
## Angular Part of Coffee shop ( Master Coffee)
### Main page for web desktop view
![Captură de ecran 2023-11-15 184605](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/ea782630-1314-4e82-ab2f-1c23bad1b561)
<br>
![Captură de ecran 2023-11-15 184629](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/72d50d25-86ab-4060-9245-f650b6ea4446)
### Main page for web mobile view
![Captură de ecran 2023-11-15 184728](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/765bb8e0-5c92-4df2-b9c1-a68e51a89ff5)
<br>
![Captură de ecran 2023-11-15 184740](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/923d3d0d-7e61-4ea6-8347-fef0d973e31b)
<br>
![Captură de ecran 2023-11-15 184806](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/71998447-c846-4997-89b1-129485354c30)

## Web REST API .Net part - Coffee Shop
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/bff32c31-ad2b-42fb-ade5-9415292bc02e)

#### The general architecture of this project is:
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/cf27022d-afae-488e-affa-b4081c2216ec)<br>
### The backend architecture is:<br>
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/415431f6-8080-454f-ad11-f84356e9f225)<br>
### This web api can be testing with tools like Postman
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/e50327d0-e732-4a2c-848b-72c22ef24900)<br>
### In next tables will you see the information about api reqests:
| Request Name | Type Request   |       Body        |   Observation|
|----------|:-------------------:|:------------:|:---------------------:|
| Register |  Post               | `{  "email": "",  `<br> ``` "firstName": "", ```<br> ```  "lastName": "", ```<br> ```  "role": "", ```<br> ```   "password": ""}  ``` | The role can be <b>User</b> or <b>Admin</b>|
| Authenticate |    Get    |   ``` {"Email": "", ```<br> ``` "Role": "", ```<br> ```  "Password": ""} ```| This work for about types of role and the user need to be register|
| GetInfoUser | Get |    ```{ "Email": "", ```<br> ``` "Role": "", ```<br> ``` "Password": ""}  ```| For this request the role need to be <b>Admin</b> and the user need to be firs login for this request |
| GetAllUsersInfo | Get |   ``` { "Email": "",```<br> ```  "Role": "", ```<br> ``` "Password": ""}  ```| For this request the role need to be <b>Admin</b> and the user need to be firs login for this request|


### The table with link for every reqest:

| Reuest Name  |  Link | 
|:--------------:|:---:|
| Register  |https://localhost:7282/api/AuthController/Auth/RegisterUser|  
| Authenticate | https://localhost:7282/api/AuthController/Auth/Authenticate |  
| GetInfoUser | https://localhost:7282/api/AuthController/Auth/GetUserInfo|
| GetAllUsersInfo |https://localhost:7282/api/AuthController/Auth/GetAllUsersInfo|
### For testing faster this requets you can use the next scripts:

