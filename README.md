
# SecureCommunication

This project demonstrates a file encryption and decryption system using RSA and AES encryption algorithms. It simulates secure communication between two parties: Alice and Bob. 

## Overview

1. **Key Pair Generation**  
   Alice generates a pair of RSA keys:  
   - Public key: Shared with Bob.  
   - Private key: Kept secret by Alice.

2. **Encryption Process** (Bob's tasks):  
   - Bob creates a one-time password (OTP).  
   - Encrypts a file using the OTP and the AES algorithm.  
   - Encrypts the OTP using Alice's public key (RSA).  
   - Sends both the encrypted file and the encrypted OTP to Alice.  

3. **Decryption Process** (Alice's tasks):  
   - Alice uses her private key to decrypt the OTP.  
   - Uses the decrypted OTP to decrypt the file.  

---

## Requirements

- **Programming Language:** C#  
- **Libraries:**  
  - `ExpressEncription` (custom encryption library for RSA and AES functionality)

---

## Usage

### Key Pair Generation
Alice generates a public-private RSA key pair using the following method:
```csharp
ExpressEncription.RSAEncription.MakeKey(publicKeyPath, privateKeyPath);
```

### Encryption Process (Bob)
1. Bob receives Alice's public key.  
2. Bob generates a random 12-character password.  
3. Bob encrypts a file (`test.zip`) using AES and the password:
   ```csharp
   ExpressEncription.AESEncription.AES_Encrypt(inputFile, password);
   ```
4. Bob encrypts the password using RSA and Alice's public key:
   ```csharp
   ExpressEncription.RSAEncription.EncryptString(plaintext, publicKeyPath);
   ```
5. Bob sends the encrypted file and the encrypted password to Alice.

### Decryption Process (Alice)
1. Alice reads the encrypted password from Bob.  
2. Alice decrypts the password using her private RSA key:
   ```csharp
   ExpressEncription.RSAEncription.DecryptString(ciphertext, privateKeyPath);
   ```
3. Alice decrypts Bob's file using the decrypted password:
   ```csharp
   ExpressEncription.AESEncription.AES_Decrypt(inputFile, password);
   ```

---

## Folder Structure

- **Alice/**  
  - `public.key`: Alice's public key.  
  - `private.key`: Alice's private key.  
  - `test.zip.aes`: Encrypted file received from Bob.  
  - `ciphertext.txt`: Encrypted password received from Bob.  

- **Bob/**  
  - `alicePublic.key`: Copy of Alice's public key.  
  - `test.zip`: Original file to be encrypted.  
  - `test.zip.aes`: Encrypted file sent to Alice.  

---

## Running the Code

1. Ensure the required folder structure (`Alice/`, `Bob/`) exists.  
2. Place the input file (`test.zip`) in `Bob/`.  
3. Run the program to execute both encryption and decryption.

---

## Notes

- This code uses a random password generator for AES encryption.
- Ensure the `ExpressEncription` library is included in the project.
- Proper error handling should be added for production use.
