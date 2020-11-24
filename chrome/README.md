# Chrome
Headless browser for integration testing using [browserless](https://browserless.io).

## Generate certificates
To generate the root certificates run the following from the `certs` directory.

1. Generate the private key.
    ```
    openssl genrsa -des3 -out ca.key 2048
    ```
    > `-des3` indicates that we want to encrypt the key with a password

2. Generate the root certificate.
    ```
    openssl req -x509 -new -nodes -key ca.key -sha256 -out ca.crt -days 365 -subj "/C=US/ST=New York/L=New York/O=None/OU=None/CN=Dev"
    ```

## Running in Docker
```
docker-compose up -d --build chrome
```

## Resources
- [SSL CA for Local Development](https://deliciousbrains.com/ssl-certificate-authority-for-local-https-development/)
- [Creating RSA Keys using OpenSSL](https://www.scottbrady91.com/OpenSSL/Creating-RSA-Keys-using-OpenSSL)
