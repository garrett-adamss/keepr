import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService{
    async getProfileById(id){
        const res = await api.get(`/api/profiles/${id}`)
        AppState.activeProfile = res.data
        logger.log('[Active Profile]', AppState.activeProfile)
    }
    async clearActive(){
        AppState.activeProfile = null;
    }
}
export const profilesService = new ProfilesService()