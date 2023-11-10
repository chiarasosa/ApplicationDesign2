import { Observable } from 'rxjs';
import { User } from '../modelos/User'; // Asegúrate de importar el modelo User adecuado

export interface Session {
  token: string;
}

export interface IUserService {

  loginUser(user: { email: string, password: string }): Observable<Session>;

  registerUser(user: User): Observable<void>;

}
