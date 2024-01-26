import { makeAutoObservable, runInAction } from "mobx";
import StateBaseStore from "./StateStores/StateStore";
import FetchingStateStore from "./StateStores/FetchingStateStore";
import LobbyList from "../services/LobbysListService";
import SuccessStateStore from "./StateStores/SuccessStateStore";
import ErrorStateStore from "./StateStores/ErrorStateStore";

interface LobbyType {
  maxrating: number;
  gameId: string;
  gameState: number;
}

class LobbyListPageStore {
  lobbys?: LobbyType[]

  state?: StateBaseStore;

  totalCount?: number;

  constructor() {
    makeAutoObservable(this)
    this.state = new StateBaseStore();
  }

  public async fetchLobbys(page: number) {
    this.state = new FetchingStateStore();
    try {
      const response = await LobbyList.fetchLobby({page: page, size: 12});
      if (response) {
        runInAction(() => {
          this.totalCount = response.totalCount;
          if ((this.lobbys && this.lobbys.length < this.totalCount) || !this.lobbys) {
            this.lobbys = [
              ...(this.lobbys ? this.lobbys : []),
              ...(response.getGamesResult.map(
                (el) => {
                  return {
                    maxrating: el.rateRange.max,
                    gameId: el.gameId,
                    gameState: el.gameState,
                  } as LobbyType
                }
              )
            )]
          } else {
            this.lobbys = [
              ...(this.lobbys ? this.lobbys : []),
            ]
          }
          this.state = new SuccessStateStore();
        })
      }
    }
    catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore();
      })
    }
  }

  public async createLobby(maxRating: number) {
    this.state = new FetchingStateStore();
    try {
      const response = await LobbyList.createLobby(maxRating);
      if (response) {
        runInAction(() => {
          this.state = new SuccessStateStore()
        })
        return response.gameId
      }
    }
    catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore();
      })
    }
  }

  public getLobbyStatus(state: number) {
    switch(state) {
      case 0: {
        return 'Ожидает'
      }
      case 1: {
        return 'Игра'
      }
      case 2: {
        return 'Игра завершена'
      }
    }
  }

  public clearStore() {
    this.lobbys = [],
    this.state = new StateBaseStore();
    this.totalCount = undefined;
  }

}

export default LobbyListPageStore;