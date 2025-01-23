import {Client, FortuneModel} from './FortuneApiClient';
import config from '../config';

const client = new Client(config.apiBaseUrl);

export const getRandomFortune = async (): Promise<FortuneModel> => {
    try{
        return await client.getRandom();
    }catch(error){
        console.error("Error fetching Fortune:", error);
        throw error;
    }
};
