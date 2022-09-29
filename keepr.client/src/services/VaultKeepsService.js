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
        logger.log("NEW VAULT KEEP", res.data)
        AppState.vaultKeeps.push(res.data)
        logger.log("[AppState.vaultKeeps]", AppState.vaultKeeps)
    }

}
export const vaultKeepsService = new VaultKeepsService()