import { ApiConnection } from "./ApiConnection";

interface lobbyResponse {
  getGamesResult: {
    gameId: string;
    rateRange: {
      min: number;
      max: number;
    }
    gameState: number;
  }[],
  totalCount: number;
}

interface createLobby {
  gameId: string;
}

interface ApplicationsRequest {
  page: number;
  size: number;
}

export interface BaseList<T> {
  entities: T[];
  totalCount: number;
}

class LobbyList {
  static async fetchLobby(
      request: Partial<ApplicationsRequest>
    ) {
    const response = await ApiConnection.get<lobbyResponse>('/games/get-all', {
      params: request,
      }
    );
    return response.data;
  }

  static async createLobby(maxrating: number) {
    const response = await ApiConnection.post<createLobby>('/games/create', {
      rateRange: {
        min: 0,
        max: maxrating,
      }
    })
    return response.data;
  }
}

export default LobbyList