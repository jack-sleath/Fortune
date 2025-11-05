import config from '../config';
import { Client, FortuneModel } from './FortuneApiClient';

const client = new Client(config.apiBaseUrl);

export const ApiHandler = {
  async getRandomFortune(): Promise<FortuneModel> {
    try {
      const fortune = await client.getRandom();
      return fortune;
    } catch (error) {
      console.error("Error fetching random fortune:", error);
      throw error;
    }
  },
};
