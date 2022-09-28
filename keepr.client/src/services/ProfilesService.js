import { api } from "./AxiosService";

class ProfilesService{
    async getProfileById(id){
           const res = await api.get(`/api/profiles/${id}`)
    }
}
export const profilesService = new ProfilesService()