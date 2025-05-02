# Buber Dinner API 

- [Buber Dinner API](#buber-dinner-api)
    - [Auth](#auth)
        - [Register](#register)
            - [Register Request](#register-request)
            - [Register Response](#register-response)
        - [Login](#login)
            - [Login Request](#login-request)
            - [Login Response](#login-response)
        
## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
    "firstName":"Napon",
    "lastName":"Tan",
    "email":"ponpon13173@gmail.com",
    "password":"1q2w3e4r"
}
```

#### Register Response

```js
200 OK
```

```json
{
    "id":"abcd-awdwd-1321cs-wdwd",
    "firstName":"Napon",
    "lastName":"Tan",
    "email":"ponpon13173@gmail.com",
    "token":"eyjhb...92bxoyzbd"
}
```

### Login