namespace UNICAP.SiteCurso.Domain.Constants
{
    public static class Messages
    {
        public const string RegisterSuccessMessage = "Registro salvo com sucesso!";
        public const string UpdateSuccessMessage = "Registro atualizado com sucesso!";
        public const string RemoveSuccessMessage = "Registro removido com sucesso!";
        public const string RemoveDbExceptionMessage = "Não foi possível remover o registro!";
        public const string RegisterDbExceptionMessage = "Não foi possível adicionar o registro!";
        public const string UpdateDbExceptionMessage = "Não houveram atualizações no registro!";
        public const string DefaultExceptionMessage = "Houve uma falha não mapeada, favor entrar em contato com a equipe de desenvolvimento";
        public const string FileExistExceptionMessage = "O anexo já existe!";
        public const string ExistServiceOrderItemMessage = "O identificador informado já existe!";
        public const string GetRegisterSuccess = "Registro obtido com sucesso!";

        #region Auth
        public static readonly string LoginEmpty = "O campo de Login encontra-se vazio";
        public static readonly string RoleEmpty = "O campo de Role encontra-se vazio ou igual a zero";
        public static readonly string NameEmpty = "O campo de Nome encontra-se vazio";
        public static readonly string EmailEmpty = "O campo de Email encontra-se vazio";
        public static readonly string EmailInvalid = "O campo de Email não foi preenchido corretamente";
        public static readonly string EmailSendPassword = "Caso seu usuário for encontrado, um e-mail chegará até você.";
        public static readonly string RecoveryPasswordTokenInvalid = "Não foi possível encontrar o Token de recuperação informado.";
        public static readonly string RecoveryPasswordTokenExpired = "Token de recuperação de acesso expirado";
        public static readonly string MatriculaEmpty = "O campo de 'Matricula encontra-se vazio";
        public static readonly string TelphoneEmpty = "O campo de 'Telefone encontra-se vazio";
        public static readonly string OrganizationEmpty = "O campo de 'Empresa encontra-se vazio";
        public static readonly string DepartmentEmpty = "O campo de 'Setor encontra-se vazio";
        public static readonly string RecoveryPasswordSuccessful = "Sua senha foi modificada com sucesso";
        public static readonly string RecoveryPasswordInvalidOldPass = "Senha anterior incorreta";
        public static readonly string DefaultMessageError = "Ocorreu um erro no servidor";
        public static readonly string ChangePasswordMessage = "Sua senha foi modificada com sucesso";
        #endregion


        #region Service
        public const string UpdateServiceNotFoundMessage = "Não foi possível encontrar o serviço informado";
        public const string HasExistSiteMessage = "O site informado já está cadastrado.";
        public const string HasExistAddressMessage = "O endereço informado já está cadastrado.";
        public const string HasExistSiteAndAddressMessage = "O endereço e o site informado já estão cadastrados.";
        public const string RegisterNotFoundMessage = "Não foi possível encontrar o registro.";
        #endregion

        #region Login
        public const string InvalidUserOrPassword = "Usuário e/ou senha inválidos";
        public const string InvalidRefreshToken = "Refresh Token inválido";
        public const string ExpiredRefreshToken = "Refresh Token expirado";
        public const string AuthenticationServerError = "Não foi possível estabelecer a conexão com o servidor de autenticação";
        #endregion

        #region Email
        public const string EmailSuccessfullySent = "E-mail enviado com sucesso";
        #endregion

        #region UserMessages
        public const string EmailOrCpfAlredyRegistered = "Email ou CPF já cadastrados, não foi possível cadastrar o usuário";
        public const string WrongPasswordMessage = "Senha incorreta";
        public const string InvalidForgotPasswordToken = "Token para troca de senha inválido";
        public const string ValidForgotPasswordToken = "Token para troca de senha válido";
        public const string EmailNotRegistered = "Este email não está cadastrado";
        public const string ManagerWorkPlace = "Usuários do tipo Gestor devem pertencer ao MPPE";
        public const string TechnicianWorkPlace = "Somente usuários do tipo Técnico podem pertencer à alguma das empresas cotratadas (Um Telecom, WorldNet e Vectra)";
        public const string AdminWorkPlace = "Usuários do tipo Admin devem pertencer exclusivamente à Vectra";
        public const string DeleteUserSuccess = "Usuário inativado com sucesso";
        #endregion

        #region Database

        public const string PersistenceDbExceptionMessage = "Não foi possível persistir as informações solicitadas.";
        public const string DeleteRegisterSuccess = "Registro inativado com sucesso";
        public const string SuccessDataTransferObject = "Dados transferidos com sucesso";
        public const string NotFoundExceptionMessage = "Informação não encontrada.";

        #endregion


        #region Session

        public const string SessionNotFound = "Sessão não encontrada";

        #endregion
    }
}
