import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    created: Date;
    gender: string;
    lastActive: any;
    photoUrl: string;
    city: string;
    country: string;
    conditions?: string;
    introduction?: string;
    lookingFor?: string;
    permissions?: string;
    photos?: Photo[];
    roleId: number;
}
