import { Observable } from 'rxjs';
import { User } from '../modelos/User'; 
import { Session } from '../interfaces/session';

export interface IUserService {

  loginUser(user: { email: string, password: string }): Observable<Session>;

  registerUser(user: User): Observable<void>;

}
