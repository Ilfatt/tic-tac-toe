import { ApiConnection } from "./ApiConnection";

interface JwtResponse {
  jwtToken: string;
}

interface UserData {
  username: string;
  userRating: number;
  userId: string;
}

class Authorization {
  static async LogIn(login: string, password: string) {
    const response = await ApiConnection.put<JwtResponse>(`/user/login`, {username: login, password: password})
    return response.data
  }

  static async Registration(login: string, password: string) {
    const response = await ApiConnection.post<JwtResponse>('/user/registration', {username: login, password: password})
    return response.data;
  }

  static async GetUserData() {
    const response = await ApiConnection.get<UserData>('/user/getUserData')
    return response.data;
  }
}

export default Authorization;