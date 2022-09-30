import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService{
    async createVaultKeep(vaultId, keepId){
        let data={
            vaultId: vaultId,
            keepId: keepId,
        }
        const res = await api.post('/api/vaultkeeps', data)
        AppState.vaultKeeps.push(res.data)
    }
    async removeKeep(id){
        const res = await api.delete(`api/vaultkeeps/` + id)
        let index = AppState.vaultKeeps.findIndex(vk => vk.vaultKeepId == id)
        AppState.vaultKeeps.splice(index, 1)
    }

}
export const vaultKeepsService = new VaultKeepsService()