export default {
  requiredValue: value => !!value || "Required.",
  maxCharacter: (value, len) =>
    (value || "").length <= len || `Maximum ${len} characters.`,
  minCharacter: (value, len) =>
    (value || "").length >= len || `Minimum ${len} characters.`,
  email: value => {
    let pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return pattern.test(value) || "Invalid e-mail.";
  },
  noSpecial: value => !/[^a-zA-Z0-9]/.test(value) || "Only alpha-numeric characters allowed.",
  noSpecialButSpace: value => !/[^a-zA-Z0-9 \b]/.test(value) || "Only alpha-numeric characters allowed."
};
