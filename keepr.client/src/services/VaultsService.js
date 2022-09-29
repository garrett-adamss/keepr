import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultsByProfileId(id){
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log('[Vaults]', res.data)
        AppState.vaults = res.data
    }
    async getOne(id){
        const res = await api.get(`api/vaults/${id}`)
        AppState.activeVault = res.data
        logger.log('[Active Vault]', AppState.activeVault)
    }
    async getVaultKeeps(id){
        logger.log(id)
        const res = await api.get(`api/vaults/${id}/keeps`)
        AppState.vaultKeeps = res.data
        logger.log('[Vault Keeps]', AppState.vaultKeeps)
    }

}
export const vaultsService = new VaultsService()