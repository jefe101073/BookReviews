import { API_BASE_URI } from "../constants";
import { Author } from "./types";

const base_url = API_BASE_URI();

export const loadAuthors = async (): Promise<Author[]> => {
    const res = await fetch(
      new Request(`${base_url}/api/authors/`)
    );
    if (!res.ok) {
      throw new Error(
        `Did not receive success from loadAuthors, status: ${res.status}, message: ${res.statusText}`
      );
    } else {
      return await res.json();
    }
  };