import {getSizeStyle, getThemeStyle, getTypeStyle, size, theme, type} from "../utils";
import styles from "./Label.module.scss";

interface LabelProps {
    text: string
    size?: size,
    type?: type,
    theme: theme
}

function Label({text, size, type, theme}: LabelProps) {

    const buttonStyle =
        styles.label + ' ' +
        getSizeStyle(size) + ' ' +
        getTypeStyle(type) + ' ' +
        getThemeStyle(theme)

    return (
        <div className={buttonStyle}>
            {text}
        </div>
    )
}

export default Label;