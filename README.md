# CoffeeShop with webapi(backend)
#### The general architecture of this project is:
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/1ca52145-1bf7-4445-9c49-b7b0e4867f47)<br>
### The backend architecture is:<br>
![image](https://github.com/MihaiPoenaru18/WebAPI_NET/assets/45234856/97c9f68f-5992-46f7-8267-7dbb631faf26)<br>
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

