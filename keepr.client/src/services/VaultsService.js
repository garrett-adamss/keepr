import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultsService {
    async getVaultsByProfileId(id) {
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log('[Vaults]', res.data)
        AppState.vaults = res.data
    }
    async getOne(id) {
        const res = await api.get(`api/vaults/${id}`)
        AppState.activeVault = res.data
        logger.log('[Active Vault]', AppState.activeVault)
    }
    async getVaultKeeps(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        AppState.vaultKeeps = res.data
        logger.log('[Vault Keeps]', AppState.vaultKeeps)
    }
    async deleteVault(id) {
        const res = await api.delete(`api/vaults/${id}`)
        logger.log('[removed vault]', res.data)
        AppState.vaults = AppState.vaults.filter(v => v.id != id)
    }
    async createVault(vaultData){
        logger.log(vaultData)
        const res = await api.post('api/vaults', vaultData)
        logger.log('[create vault, res.data]', res.data)
        AppState.vaults.push(res.data)
       logger.log('[AppState.vaults]', AppState.vaults)
    }

}
export const vaultsService = new VaultsService()