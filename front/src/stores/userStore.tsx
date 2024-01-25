import { makeAutoObservable } from "mobx";

class UserStore {
    username?: string

    rating?: string;

    constructor() {
        makeAutoObservable(this);
      }
}

export default UserStore;