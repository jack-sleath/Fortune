// src/api/ApiHandler.ts
import config from '../config';
import { Client, FortuneModel } from './FortuneApiClient';

// Instantiate the NSwag client using the base URL from the config.
const client = new Client(config.apiBaseUrl);

export const ApiHandler = {
  /**
   * Fetch a random fortune using the NSwag-generated client.
   * Returns a Promise that resolves to a FortuneModel.
   */
  async getRandomFortune(): Promise<FortuneModel> {
    try {
      const fortune = await client.getRandom();
      return fortune;
    } catch (error) {
      console.error("Error fetching random fortune:", error);
      throw error;
    }
  },

  // Wrap additional endpoints as needed...
};
