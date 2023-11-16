export class User {
  userID: number;
  userName: string;
  password: string;
  email: string;
  role: string;
  address: string;

  constructor(userID: number, userName: string, password: string, email: string, role: string, address: string) {
    this.userID = userID;
    this.userName = userName;
    this.password = password;
    this.email = email;
    this.role = role;
    this.address = address;
  }
}
