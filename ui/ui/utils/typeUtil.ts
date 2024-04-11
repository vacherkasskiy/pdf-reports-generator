import styles from "@/ui/general/themes.module.scss";

export type type = 'fill' | 'outline';

const getTypeStyle = (type: type | undefined): string => {
    switch (type) {
        case undefined:
            return styles.Type_fill
        case 'fill':
            return styles.Type_fill
        case 'outline':
            return styles.Type_outline
    }
}

export default getTypeStyle;