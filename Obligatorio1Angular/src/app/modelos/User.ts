export class User {
    username: string;
    password: string;
    email: string;
    role: string;
    address: string;
  
    constructor(username: string, password: string, email: string, role: string, address: string) {
      this.username = username;
      this.password = password;
      this.email = email;
      this.role = role;
      this.address = address;
    }
  }  

