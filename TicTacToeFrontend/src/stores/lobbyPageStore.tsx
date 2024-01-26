import { makeAutoObservable, runInAction } from "mobx";
import StateBaseStore from "./StateStores/StateStore";
import StateStore from "./StateStores/StateStore";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import FetchingStateStore from "./StateStores/FetchingStateStore";
import { appApiUrl, getToken } from "../services/ApiConnection";

class LobbyPageStore {
  ownerId?: string;

  userId?: string;

  field?: number[];

  oponnetnName?: string;

  socket?: HubConnection;

  ownerName?: string;

  state?: StateStore;

  constructor() {
    makeAutoObservable(this);
    this.state = new StateBaseStore();
  }

  async initSocket() {
    try {
      this.state = new FetchingStateStore();
      this.socket = new HubConnectionBuilder()
      .withUrl(
        `${appApiUrl}/api/room`, {accessTokenFactory: () => getToken()}
      ).build();
    }
    catch (error) {
      runInAction(() => {

      })
    }
  }

  public clearStore() {
    this.ownerId = undefined;
    this.userId = undefined;
    this.field = [];
    this
  }

}

export default LobbyPageStore;