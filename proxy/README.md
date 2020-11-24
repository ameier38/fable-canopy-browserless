# Proxy
[Reverse Proxy](https://www.cloudflare.com/learning/cdn/glossary/reverse-proxy/) using
[NGINX](https://nginx.com).

The reverse proxy will enable SSL by decrypting incoming requests and encrypting outgoing requests.

## Generate certificates
To generate the proxy certificates run the following commands from the `certs` directory.

1. Generate the private key.
    ```
    openssl genrsa -out proxy.key 2048
    ```

2. Generate the certificate signing request (CSR).
    ```
    openssl req -new -key proxy.key -out proxy.csr -subj "/C=US/ST=New York/L=New York/O=None/OU=None/CN=proxy"
    ```

3. Generate the certificate using our self signed root certificates.
    ```
    openssl x509 -req -in proxy.csr -CA ../../chrome/certs/ca.crt -CAkey ../../chrome/certs/ca.key -CAcreateserial -out proxy.crt -days 365 -sha256 -extfile proxy.ext
    ```
