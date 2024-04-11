import sizes from "@/ui/general/sizes.module.scss";

export type size = 's' | 'm' | 'l'

const getSizeStyle = (size: size | undefined): string => {
    switch (size) {
        case undefined:
            return sizes.Size_m
        case 's':
            return sizes.Size_s
        case 'm':
            return sizes.Size_m
        case 'l':
            return sizes.Size_l
    }
}

export default getSizeStyle;