export class LoginData {
  Username: string;
  Password: string;
}

export class Organization {
  id: number;
  name: string;
  address: string
  contactno: string;
  contactemail: string;
  contactperson: string;
  registeredon: string;
  timezone_mins: number
  expiry: string;
  isactive: boolean;
  totalAdmin: number;
  totalUser: number;
}

export class OrganizationUsers {
  id: number;
  fullname: string;
  usertypeid: number;
  hospitalName: string;
}

export class UserResult {
  dateTimeSession: string;
  id: number;
  resultJSon: string;
  scenarioname: string;
  userid: string;
}
