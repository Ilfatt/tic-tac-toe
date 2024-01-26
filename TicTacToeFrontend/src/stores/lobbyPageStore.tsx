import { makeAutoObservable } from "mobx";
import StateBaseStore from "./StateStores/StateStore";
import StateStore from "./StateStores/StateStore";

class LobbyPageStore {
  ownerId?: string;

  userId?: string;

  field?: number[];

  oponnetnName?: string;

  ownerName?: string;

  state?: StateStore;

  constructor() {
    makeAutoObservable(this);
    this.state = new StateBaseStore();
  }

  public clearStore() {
    this.ownerId = undefined;
    this.userId = undefined;
    this.field = [];
    this
  }

}

export default LobbyPageStore;