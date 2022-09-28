import { api } from "./AxiosService"

class VaultsService{
    async getVaultsByProfileId(id){
        const res = await api.get(`api/profiles/${id}/vaults`)
        logger.log('[vaults]', res.data)
    }
}
export const vaultsService = new VaultsService()