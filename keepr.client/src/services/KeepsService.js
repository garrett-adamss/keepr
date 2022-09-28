import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{

    async getKeeps(){
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
        logger.log('[keeps appstate]', AppState.keeps)
    }
    async getOne(id){
        const res = await api.get(`api/keeps/${id}`)
        AppState.activeKeep = res.data
        logger.log('[Active Keep]', AppState.activeKeep)
    }
}

export const keepsService = new KeepsService() 