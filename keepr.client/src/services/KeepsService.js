import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{

    async getKeeps(){
        const res = await api.get('api/keeps')
        AppState.keeps = res.data
        logger.log('[keeps appstate]', AppState.keeps)
    }
    async getKeepsByProfileId(id){
        const res = await api.get(`api/profiles/${id}/keeps`)
        AppState.keeps = res.data
        logger.log('[Keeps]', res.data)
    }
    async getOne(id){
        const res = await api.get(`api/keeps/${id}`)
        AppState.activeKeep = res.data
        logger.log('[Active Keep]', AppState.activeKeep)
    }
    async deleteKeep(id){
        const res = await api.delete(`api/keeps/${id}`)
        logger.log('Deleting Keep')
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }
    async createKeep(keepData){
        const res = await api.post('api/keeps', keepData)
        logger.log("[res.data]", res.data)
        AppState.keeps.push(res.data)
    }
}

export const keepsService = new KeepsService() 