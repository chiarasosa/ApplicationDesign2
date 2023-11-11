import { Observable } from 'rxjs';
import { User } from '../modelos/User'; // Asegúrate de importar el modelo User adecuado
import { Session } from 'src/app/interfaces/Session';

export interface IUserService {

  loginUser(user: { email: string, password: string }): Observable<Session>;

  registerUser(user: User): Observable<void>;

}
