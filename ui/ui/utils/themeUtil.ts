import themes from "@/ui/general/themes.module.scss";

export type theme = 'red' | 'dark' | 'blue' | 'orange' | 'green';

const getThemeStyle = (theme: theme): string => {
    switch (theme) {
        case 'red':
            return themes.Theme_red
        case 'green':
            return themes.Theme_green
        case 'dark':
            return themes.Theme_dark
        case 'blue':
            return themes.Theme_blue
        case 'orange':
            return themes.Theme_orange
    }
}

export default getThemeStyle;